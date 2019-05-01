import { Injectable, EventEmitter } from '@angular/core';

/** Operates with displaying notifications */
@Injectable({
  providedIn: 'root'
})
export class NotificationsService {
  /** Current message */
  message = '';
  /** Current message timeout identifier */
  private messageTimeoutId_;
  /** Emits on message change */
  messageChange = new EventEmitter<string>();
  /**
   * Show a new message.
   * New message overrides former one.
   * @param msg message to display
   * @param timeout message displaying time
   * @param persistent identifies if message should stay persistent.
   *    This function omits timeout in case persistent=true
   */
  showMessage(msg: string, timeout = 3000, persistent = false) {
    this.message = msg;
    this.messageChange.emit(msg);
    if (this.messageTimeoutId_) 
      clearTimeout(this.messageTimeoutId_);
    if (!persistent)
      this.messageTimeoutId_ = setTimeout(() => {
        this.message = '';
        this.messageChange.emit('');
      }, timeout);
  }
}
