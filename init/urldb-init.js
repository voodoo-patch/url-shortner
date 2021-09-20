db.createUser({
    user: 'admin',
    pwd: 'password',
    roles: [
        {
            role: 'readWrite',
            db: 'urldb',
        },
    ],
});

db = new Mongo().getDB("urldb");

db.createCollection('tinyurls', { capped: false });
db.tinyurls.createIndex({ "ShortUrl": 1 }, { unique: true });