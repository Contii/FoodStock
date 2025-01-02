// This script is specifically for the published version of the service worker and imports the base service-worker.js script.

self.importScripts('./service-worker.js');

self.addEventListener('install', event => {
    console.log('Service worker (published) installing...');
    self.skipWaiting();
});

self.addEventListener('activate', event => {
    console.log('Service worker (published) activating...');
});

self.addEventListener('fetch', event => {
    console.log('Fetching (published):', event.request.url);
    event.respondWith(
        caches.match(event.request).then(response => {
            return response || fetch(event.request);
        })
    );
});