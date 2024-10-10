// vite.config.js
import { defineConfig } from 'vite';
import angular from '@vitejs/plugin-angular';

export default defineConfig({
  plugins: [angular()],
  optimizeDeps: {
    include: ['chart.js']
  }
});
