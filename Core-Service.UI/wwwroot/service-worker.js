/* This service worker script handles the installation, activation, and fetch (local cache) events.
It logs messages to the console during these events and forces the service worker to become active immediately upon installation.
It is registered in the ./wwwroot/index.html file. */

self.addEventListener('install', event => {
    console.log('Service worker installing...');
    self.skipWaiting();
});

self.addEventListener('activate', event => {
    console.log('Service worker activating...');
});

self.addEventListener('fetch', event => {
    console.log('Fetching:', event.request.url);
    event.respondWith(
        caches.match(event.request).then(response => {
            return response || fetch(event.request);
        })
    );
});