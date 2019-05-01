import { Injectable, EventEmitter } from '@angular/core';
import { of, Observable, throwError } from 'rxjs';
import { Coin } from 'src/app/models/coin/coin';
import { HttpClient, HttpParams } from '@angular/common/http';
import { apiCoinCrud, deviceId, apiChangeCoinAmount } from '../api-routes';
import { tap } from 'rxjs/operators';

/**
 * Contains all coins-related operations
 */
@Injectable({
  providedIn: 'root'
})
export class CoinsService {

  constructor(
    private http: HttpClient
  ) { 
    this.getCoins()
      .subscribe((coins: Coin[]) => {
        // Sord by desc
        this.ownedCoins = this.sortCoins_(coins);
        this.ownedCoinsChange.emit(this.ownedCoins);
      });
  }
  /** Current user cash */
  userCash: number = 0;
  /** Currently owned coins with all related information */
  ownedCoins: Coin[];
  /** Emits each time user cash changes */
  userCashChange = new EventEmitter<number>();
  /** Emits on new coins created or deleted */
  ownedCoinsChange = new EventEmitter<Coin[]>();

  /**
   * Sort coins by value (desc)
   * @param coins coins to sort
   */
  private sortCoins_(coins: Coin[]) {
    return coins.sort((a: Coin, b: Coin) => {
      if (b.value > a.value) return 1
      return -1
    });
  }
  /** Get all device coins info */
  getCoins(): Observable<Coin[]> {
    const options = {
      params: new HttpParams()
        .set('deviceId', deviceId)
    };

    return this.http.get<Coin[]>(apiCoinCrud, options);
  }
  /**
   * Update coin info
   * @param coin new coin version
   */
  updateCoin(coin: Coin): Observable<Coin> {
    return this.http.put<Coin>(apiCoinCrud, coin, { withCredentials: true })
      .pipe(
        tap((coin: Coin) => {
          const i = this.ownedCoins.findIndex(c => c.id === coin.id);
          this.ownedCoins[i] = coin;
          this.ownedCoins = this.sortCoins_(this.ownedCoins);
          this.ownedCoinsChange.emit(this.ownedCoins);
        })
      );
  }
  /**
   * Change coin amount for given value.
   * Positive value leads to incresing,
   * negative to descreasing
   * @param coin target coin
   * @param amount change amount
   */
  changeAmount(coin: Coin, amount: number): Observable<any> {
    const options = {
      params: new HttpParams()
        .set('coinId', coin.id.toString())
        .set('amount', amount.toString())
    };

    return this.http.get(apiChangeCoinAmount, options)
      .pipe(
        tap(() => {
          coin.quantity += amount;
        })
      );
  }
  /**
   *  Returns true if we can return change for given cash inside dispencer
   * @param cash cash to check out
   */
  private canGrabChange_(cash = this.userCash): boolean {
    for (let coinType of this.ownedCoins) {
      cash -= coinType.value * Math.min(
        Math.floor(cash / coinType.value),
        coinType.quantity
      );
      if (!cash) return true;
    }
    return false;
  }
  /** Return change for current user cash */
  grabChange(): Observable<any> {
    if (this.canGrabChange_()) {
      // All coins are ordered. Return least coin amount possible
      for (let coin of this.ownedCoins) {
        let coinsRequired = Math.min(
          Math.floor(this.userCash / coin.value),
          coin.quantity
        );
        if (coinsRequired === 0) continue;
        /** Request options */
        let options = {
          params: new HttpParams()
            .set('coinId', coin.id.toString())
            .set('amount', `-${coinsRequired}`)
        }
        this.http.get(apiChangeCoinAmount, options)
          .subscribe();
        
        this.userCash -= coin.value * coinsRequired;
        coin.quantity -= coinsRequired;
        // Return if all cash in given up
        if (!this.userCash) {
          this.userCashChange.emit(this.userCash);
          this.ownedCoinsChange.emit(this.ownedCoins);
          return of({});
        }
      }
    }
    return throwError('');
  }
  /**
   * Change current user cash for given amount.
   * @param value amount of cash to change
   */
  changeCash(value: number): void {
    this.userCash += value;
    this.userCashChange.emit(this.userCash);
  }
  /**
   * Create new coin.
   * @param coin new coin
   */
  createCoin(coin: Coin): Observable<any> {
    return this.http.post(apiCoinCrud, coin, { withCredentials: true })
      .pipe(
        tap((coin: Coin) => {
          this.ownedCoins.push(coin);
          this.ownedCoins = this.sortCoins_(this.ownedCoins);
          this.ownedCoinsChange.emit(this.ownedCoins);
        })
      )
  }
  /**
   * Delete coin
   * @param coin coin to delete
   */
  deleteCoin(coin: Coin): Observable<any> {
    const options = {
      params: new HttpParams()
        .set('coinId', coin.id.toString()),
      withCredentials: true
    }

    return this.http.delete(apiCoinCrud, options)
      .pipe(
        tap(() => {
          let i = this.ownedCoins.findIndex(c => c.id === coin.id);
          this.ownedCoins.splice(i, 1);
        })
      );
  }
}
