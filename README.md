# url-shortener

## Run instructions

First build the containers by running

`$ docker-compose build`

Then run the following commmand

`$ docker-compose up`

The following containers will be created and run:

- web: container that serves client requests exposing APIs on `localhost:5020`
- urldb: mongodb instance that stores shortenings requested by clients
- kgs: keygen service container that runs a deamon responsible of creating keys to be used as url alias
- keydb: mongodb instance that stores ready-to-use fresh keys (url aliases)