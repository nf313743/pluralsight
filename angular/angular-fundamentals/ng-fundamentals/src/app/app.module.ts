import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { NavBarComponent } from "./nav/navbar.component";
import { ToastrService } from "./common/toastr.service";
import { appRoutes } from "./routes";
import { Error404Component } from "./errors/Error404Component";

import {
  CreateEventComponent,
  EventListResolver,
  EventRouteActivator,
  EventDetailsComponent,
  EventsListComponent,
  EventThumbnailComponent,
  EventService
} from "./events/index";
import { EventsAppComponent } from "./events-app.component";

@NgModule({
  imports: [BrowserModule, RouterModule.forRoot(appRoutes)],
  declarations: [
    EventsAppComponent,
    EventsListComponent,
    EventThumbnailComponent,
    NavBarComponent,
    EventDetailsComponent,
    CreateEventComponent,
    Error404Component
  ],
  providers: [
    EventService,
    ToastrService,
    EventRouteActivator,
    EventListResolver
  ],
  bootstrap: [EventsAppComponent]
})
export class AppModule {}
