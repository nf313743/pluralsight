import { Routes } from "@angular/router";
import { Error404Component } from "./errors/Error404Component";

import {
  EventsListComponent,
  EventDetailsComponent,
  CreateEventComponent,
  EventListResolver,
  CreateSessionComponent,
} from "./events/index";
import { EventResolver } from "./events/event-resolver.service";

export const appRoutes: Routes = [
  { path: "events/new", component: CreateEventComponent },
  {
    path: "events",
    component: EventsListComponent,
    resolve: { events: EventListResolver },
  },
  {
    path: "events/:id",
    component: EventDetailsComponent,
    resolve: { event: EventResolver },
  },
  { path: "events/session/new", component: CreateSessionComponent },
  { path: "404", component: Error404Component },
  { path: "", redirectTo: "/events", pathMatch: "full" },
  { path: "user", loadChildren: "./user/user.module#UserModule" },
];
