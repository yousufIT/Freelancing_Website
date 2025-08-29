import { ApplicationConfig } from '@angular/core';
import { PreloadAllModules, provideRouter, withPreloading, withViewTransitions, withInMemoryScrolling, withHashLocation } from '@angular/router';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { authInterceptor } from './interceptors/auth.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideAnimations(),
    provideHttpClient(
      withInterceptors([authInterceptor])
    ),
    provideRouter(
      routes,
      withViewTransitions(),
      withPreloading(PreloadAllModules),
      withInMemoryScrolling({ scrollPositionRestoration: 'enabled' }),
      withHashLocation()  
    )
  ]
};
