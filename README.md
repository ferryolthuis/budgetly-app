# About this project
...

# Setup
1. Install Docker for Windows
2. In Rider, go to Run -> Edit Configurations. Create a new run configuration with the following settings:
   - Type: Docker -> Docker Compose
   - Compose files: ```./docker/docker-compose.yml; ./docker/docker-compose.override.yml;```
   - Services: Select all services in the project (named ```something-service```)
   - Additional options: Attach to none & build always
3. Hit debug, you can now debug the applications. All required services are running in docker. Required credentials, local ports, etc. can be found in the ```docker\``` folder in the root of the solution.
    - Postgres: Your database server
    - PGAdmin: For browsing your databases
    - Seq: Your logging provider
    
# Architecture & Frameworks
...