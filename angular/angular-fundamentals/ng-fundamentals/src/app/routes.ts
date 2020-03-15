import { Routes } from "@angular/router";
import { Error404Component } from "./errors/Error404Component";

import {
  EventsListComponent,
  EventDetailsComponent,
  CreateEventComponent,
  EventRouteActivator,
  EventListResolver,
  CreateSessionComponent
} from "./events/index";

export const appRoutes: Routes = [
  { path: "events/new", component: CreateEventComponent },
  {
    path: "events",
    component: EventsListComponent,
    resolve: { events: EventListResolver }
  },
  {
    path: "events/:id",
    component: EventDetailsComponent,
    canActivate: [EventRouteActivator]
  },
  { path: "events/session/new", component: CreateSessionComponent },
  { path: "404", component: Error404Component },
  { path: "", redirectTo: "/events", pathMatch: "full" },
  { path: "user", loadChildren: "./user/user.module#UserModule" }
];
