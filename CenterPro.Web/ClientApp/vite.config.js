import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

export default defineConfig({
  plugins: [react()],
  base: '/react/',
  build: {
    outDir: '../wwwroot/react',
    emptyOutDir: true,
    rollupOptions: {
      output: {
        entryFileNames: 'centerpro.js',
        chunkFileNames: 'chunks/[name].js',
        assetFileNames: (asset) => asset.name?.endsWith('.css') ? 'centerpro.css' : 'assets/[name][extname]'
      }
    }
  }
});
