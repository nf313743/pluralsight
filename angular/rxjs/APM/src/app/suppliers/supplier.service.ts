import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { throwError, Observable, of, merge } from 'rxjs';
import { Supplier } from './supplier';
import {
  catchError,
  concatMap,
  mergeMap,
  shareReplay,
  switchMap,
  tap,
} from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class SupplierService {
  suppliersUrl = 'api/suppliers';

  constructor(private http: HttpClient) {
    // this.supplierWithConcatMap$.subscribe((item) =>
    //   console.log('concat map result', item)
    // );
    // this.supplierWithMergeMap$.subscribe((item) =>
    //   console.log('merge map result', item)
    // );
    // this.supplierWithSwitchMap$.subscribe((item) =>
    //   console.log('switch map result', item)
    // );
  }

  suppliers$ = this.http.get<Supplier[]>(this.suppliersUrl).pipe(
    tap((data) => console.log('suppliers', JSON.stringify(data))),
    shareReplay(1),
    catchError(this.handleError)
  );

  supplierWithConcatMap$ = of(1, 5, 8).pipe(
    tap((id) => console.log('concatMap source Observable', id)),
    concatMap((id) => this.http.get<Supplier>(`${this.suppliersUrl}/${id}`))
  );

  supplierWithMergeMap$ = of(1, 5, 8).pipe(
    tap((id) => console.log('mergeMap source Observable', id)),
    mergeMap((id) => this.http.get<Supplier>(`${this.suppliersUrl}/${id}`))
  );

  supplierWithSwitchMap$ = of(1, 5, 8).pipe(
    tap((id) => console.log('switchMap source Observable', id)),
    switchMap((id) => this.http.get<Supplier>(`${this.suppliersUrl}/${id}`))
  );

  private handleError(err: any): Observable<never> {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Backend returned code ${err.status}: ${err.body.error}`;
    }
    console.error(err);
    return throwError(errorMessage);
  }
}
