@echo OFF

echo BUILD.........................
docker build -t registry.heroku.com/hbookstore/web .

echo PUSH..........................
call heroku container:push web -a hbookstore

echo RELEASE.......................
call heroku container:release web -a hbookstore


echo OPEN..........................
call heroku open -a hbookstore

echo DONE..........................
