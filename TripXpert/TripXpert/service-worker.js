var cacheName = 'v1';
var cachedFiles = [
    '/'
];

var getPath = function (href) {
    var l = new URL(href);
    return l.pathname;
};

self.addEventListener('install', function (e) {
    console.log('Installed!');

    e.waitUntil(
        caches.open(cacheName).then(function (cache) {
            console.log("Caching...")
            return cache.addAll(cachedFiles);
        })
    )
})

self.addEventListener('activate', function (e) {
    console.log('Activated!');

    e.waitUntil(
        caches.keys().then(function (cachedNames) {
            cachedNames.forEach(function (currentCache) {
                if (currentCache != cacheName) {
                    console.log('deleting cache')
                    caches.delete(currentCache);
                }
            }, this);
        })
    )
})

self.addEventListener('fetch', function (e) {
    if (cachedFiles.indexOf(getPath(e.request.url)) != -1) {
        e.respondWith(
            caches.match(e.request).then(function (resp) {
                return resp || fetch(e.request).then(function (response) {
                    caches.open('v1').then(function (cache) {
                        cache.put(e.request, response.clone());
                    });
                    return response;
                });
            }).catch(function () {
                return caches.match('/');
            })
        );
    }    
});
