import { Injectable } from "@angular/core";
import { UserSettings } from "./user-settings";
import { HttpClient } from "@angular/common/http";
import { Observable, of } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class DataService {
  constructor(private http: HttpClient) {}

  postUserSettingsForm(userSettings: UserSettings): Observable<any> {
    return this.http.post(
      "https://putsreq.com/uzfS82OXnoqPZGYialRm",
      userSettings
    );
  }

  getSubscriptionTypes(): Observable<string[]> {
    return of(["Monthly", "Annual", "Lifetime"]);
  }
}
