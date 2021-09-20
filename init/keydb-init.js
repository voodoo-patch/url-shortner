db.createUser({
    user: 'admin',
    pwd: 'password',
    roles: [
        {
            role: 'readWrite',
            db: 'keydb',
        },
    ],
});

db = new Mongo().getDB("keydb");

db.createCollection('fresh', { capped: false });
db.fresh.createIndex({ "key": 1 }, { unique: true });
db.createCollection('taken', { capped: false });
db.taken.createIndex({ "key": 1 }, { unique: true });