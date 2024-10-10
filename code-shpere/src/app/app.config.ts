import { ApplicationConfig } from '@angular/core';
import { PreloadAllModules, provideRouter, withPreloading, withViewTransitions, withInMemoryScrolling } from '@angular/router';
import { routes } from './app.routes';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideClientHydration } from '@angular/platform-browser';
import { provideServerRendering } from '@angular/platform-server';
export const appConfig: ApplicationConfig = {
  providers: [
    provideAnimations(),
    provideServerRendering(),
    provideClientHydration(),
    provideHttpClient(),
    provideRouter(routes, withViewTransitions(), withPreloading(PreloadAllModules),
      withInMemoryScrolling({
      scrollPositionRestoration: 'enabled',
    })
  )]
};
