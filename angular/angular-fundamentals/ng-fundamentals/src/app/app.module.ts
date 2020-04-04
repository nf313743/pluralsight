import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { NavBarComponent } from "./nav/navbar.component";
import { appRoutes } from "./routes";
import { Error404Component } from "./errors/Error404Component";

import {
  CreateEventComponent,
  EventListResolver,
  EventRouteActivator,
  EventDetailsComponent,
  EventsListComponent,
  EventThumbnailComponent,
  EventService,
  CreateSessionComponent,
  SessionListComponent,
  DurationPipe,
} from "./events/index";
import {
  JQ_TOKEN,
  TOASTR_TOKEN,
  Toastr,
  CollapsibleWellComponent,
  SimpleModalComponent,
  ModalTriggerDirective,
} from "./common/index";
import { EventsAppComponent } from "./events-app.component";
import { AuthService } from "./user/auth.service";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

let toastr: Toastr = window["toastr"];
let jQuery = window["$"];

@NgModule({
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes),
    FormsModule,
    ReactiveFormsModule,
  ],
  declarations: [
    EventsAppComponent,
    EventsListComponent,
    EventThumbnailComponent,
    NavBarComponent,
    EventDetailsComponent,
    CreateEventComponent,
    Error404Component,
    CreateSessionComponent,
    SessionListComponent,
    CollapsibleWellComponent,
    DurationPipe,
    SimpleModalComponent,
    ModalTriggerDirective
  ],
  providers: [
    EventService,
    EventRouteActivator,
    EventListResolver,
    AuthService,
    {
      provide: TOASTR_TOKEN,
      useValue: toastr,
    },
    {
      provide: JQ_TOKEN,
      useValue: jQuery,
    },
  ],
  bootstrap: [EventsAppComponent],
})
export class AppModule {}
