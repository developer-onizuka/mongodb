# 1. Create new Project as console
   ```
   $ dotnet new console -o mongodb
   ```
# 2. Download Program.cs
   ```
   $ cd mongodb/
   $ rm Program.cs
   $ wget https://raw.githubusercontent.com/developer-onizuka/mongodb/master/Program.cs
   $ wget https://raw.githubusercontent.com/developer-onizuka/mongodb/master/import.json
   $ wget https://raw.githubusercontent.com/developer-onizuka/mongodb/master/update.json
   $ dotnet add package MongoDB.Driver --version 2.10.0
   ```
# 3. Run MongoDB by using Docker
   ```
   $ sudo docker images
   REPOSITORY          TAG                 IMAGE ID            CREATED             SIZE
   ubuntu              latest              bb0eaf4eee00        5 months ago        72.9MB
   centos              latest              0d120b6ccaa8        6 months ago        215MB
   zookeeper           latest              6ad6cb039dfa        6 months ago        252MB
   mongo               latest              aa22d67221a0        6 months ago        493MB
   $ sudo docker run -itd -p 27017:27017 --name="mongodb" mongo:latest
   $ sudo docker ps -a
   CONTAINER ID        IMAGE               COMMAND                  CREATED             STATUS              PORTS                      NAMES
   e0951416f159        mongo:latest        "docker-entrypoint.s…"   31 minutes ago      Up 31 minutes       0.0.0.0:27017->27017/tcp   mongodb
   ```
# 4. Test
   ```
   $ dotnet run query
   (no output)
   $ dotnet run create
   6028979b3baa6216af46fee9
   $ dotnet run query
   6028979b3baa6216af46fee9,apple,100,0,1/1/0001 12:00:00 AM
   $ dotnet run deleteall
   ```
# 5. Import a json file to the DB
   ```
   $ cat import.json
   {"name":"raspberry","price":300,"stock":1},
   {"name":"lemon","price":350,"stock":1},
   {"name":"apple","price":100,"stock":1},
   {"name":"orange","price":150,"stock":1},
   {"name":"melon","price":180,"stock":1},
   {"price":800, "name":"banana"}
   $ dotnet run import
   6028986720628c1790944900
   6028986720628c1790944901
   6028986720628c1790944902
   6028986720628c1790944903
   6028986720628c1790944904
   6028986720628c1790944905
   $ dotnet run query
   6028986720628c1790944900,raspberry,300,1,2/14/2021 3:26:31 AM
   6028986720628c1790944901,lemon,350,1,2/14/2021 3:26:31 AM
   6028986720628c1790944902,apple,100,1,2/14/2021 3:26:31 AM
   6028986720628c1790944903,orange,150,1,2/14/2021 3:26:31 AM
   6028986720628c1790944904,melon,180,1,2/14/2021 3:26:31 AM
   6028986720628c1790944905,banana,800,0,2/14/2021 3:26:31 AM
   ```
# 6. Update records by json file
   ```
   $ $ cat update.json 
   {"name":"raspberry","price":320,"stock":100},
   {"name":"melon","price":190,"stock":0},
   {"price":810, "name":"banana","stock":1}
   $ dotnet run update
   $ dotnet run query
   6028986720628c1790944900,raspberry,320,100,2/14/2021 3:31:28 AM
   6028986720628c1790944901,lemon,350,1,2/14/2021 3:26:31 AM
   6028986720628c1790944902,apple,100,1,2/14/2021 3:26:31 AM
   6028986720628c1790944903,orange,150,1,2/14/2021 3:26:31 AM
   6028986720628c1790944904,melon,190,0,2/14/2021 3:31:28 AM
   6028986720628c1790944905,banana,810,1,2/14/2021 3:31:28 AM
   ```

# 7. Look directory into the DB
   ```
   $ sudo docker exec -it mongodb bash
   root@e0951416f159:/# mongo
   MongoDB shell version v4.4.0
   connecting to: mongodb://127.0.0.1:27017/?compressors=disabled&gssapiServiceName=mongodb
   Implicit session: session { "id" : UUID("89a58b88-f54b-4ee5-b7a3-355a2d0e9c27") }
   MongoDB server version: 4.4.0
   Welcome to the MongoDB shell.
   For interactive help, type "help".
   For more comprehensive documentation, see
	https://docs.mongodb.com/
   Questions? Try the MongoDB Developer Community Forums
	https://community.mongodb.com
   ---
   The server generated these startup warnings when booting: 
           2021-02-14T02:50:01.060+00:00: Using the XFS filesystem is strongly recommended with the WiredTiger storage engine. See http://dochub.mongodb.org/core/prodnotes-filesystem
           2021-02-14T02:50:01.816+00:00: Access control is not enabled for the database. Read and write access to data and configuration is unrestricted
           2021-02-14T02:50:01.817+00:00: Soft rlimits too low
           2021-02-14T02:50:01.817+00:00:         currentValue: 1024
           2021-02-14T02:50:01.817+00:00:         recommendedMinimum: 64000
   ---
   ---
           Enable MongoDB's free cloud-based monitoring service, which will then receive and display
           metrics about your deployment (disk utilization, CPU, operation statistics, etc).

           The monitoring data will be available on a MongoDB website with a unique URL accessible to you
           and anyone you share the URL with. MongoDB may use this information to make product
           improvements and to suggest MongoDB products and deployment options to you.

           To enable free monitoring, run the following command: db.enableFreeMonitoring()
           To permanently disable this reminder, run the following command: db.disableFreeMonitoring()
   ---
   > show dbs
   admin   0.000GB
   config  0.000GB
   local   0.000GB
   mydb    0.000GB
   > use mydb
   switched to db mydb
   > show collections
   Fruit
   > db.Fruit.find()
   { "_id" : ObjectId("6028986720628c1790944900"), "name" : "raspberry", "price" : 320, "stock" : 100, "last_update" : ISODate("2021-02-14T03:31:28.944Z") }
   { "_id" : ObjectId("6028986720628c1790944901"), "name" : "lemon", "price" : 350, "stock" : 1, "last_update" : ISODate("2021-02-14T03:26:31.771Z") }
   { "_id" : ObjectId("6028986720628c1790944902"), "name" : "apple", "price" : 100, "stock" : 1, "last_update" : ISODate("2021-02-14T03:26:31.771Z") }
   { "_id" : ObjectId("6028986720628c1790944903"), "name" : "orange", "price" : 150, "stock" : 1, "last_update" : ISODate("2021-02-14T03:26:31.771Z") }
   { "_id" : ObjectId("6028986720628c1790944904"), "name" : "melon", "price" : 190, "stock" : 0, "last_update" : ISODate("2021-02-14T03:31:28.944Z") }
   { "_id" : ObjectId("6028986720628c1790944905"), "name" : "banana", "price" : 810, "stock" : 1, "last_update" : ISODate("2021-02-14T03:31:28.944Z") }
   > db.Fruit.find({price : 150})
   { "_id" : ObjectId("6028986720628c1790944903"), "name" : "orange", "price" : 150, "stock" : 1, "last_update" : ISODate("2021-02-14T03:26:31.771Z") }
   > db.Fruit.find({price :{$gte : 150}})
   { "_id" : ObjectId("6028986720628c1790944900"), "name" : "raspberry", "price" : 320, "stock" : 100, "last_update" : ISODate("2021-02-14T03:31:28.944Z") }
   { "_id" : ObjectId("6028986720628c1790944901"), "name" : "lemon", "price" : 350, "stock" : 1, "last_update" : ISODate("2021-02-14T03:26:31.771Z") }
   { "_id" : ObjectId("6028986720628c1790944903"), "name" : "orange", "price" : 150, "stock" : 1, "last_update" : ISODate("2021-02-14T03:26:31.771Z") }
   { "_id" : ObjectId("6028986720628c1790944904"), "name" : "melon", "price" : 190, "stock" : 0, "last_update" : ISODate("2021-02-14T03:31:28.944Z") }
   { "_id" : ObjectId("6028986720628c1790944905"), "name" : "banana", "price" : 810, "stock" : 1, "last_update" : ISODate("2021-02-14T03:31:28.944Z") }
   ```
