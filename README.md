# About this project
...

# Setup
1. Install Docker for Windows
2. In Rider, go to Run -> Edit Configurations. Create a new run configuration with the following settings:
   - Type: Docker -> Docker Compose
   - Compose files: ```./docker/docker-compose.yml; ./docker/docker-compose.override.yml;```
   - Services: Select all services in the project (named ```something-service```)
   - Additional options: Attach to none & build always
3. Hit debug, you can now debug the applications. All required services are running in docker
    - Postgres: Your database server, see .env for default credentials and docker-compose.override.yml for local port
    - PGAdmin: For browsing your databases, see .env for default credentials and docker-compose.override.yml for local port
    - Seq: Your logging provider, see .env for default credentials and docker-compose.yml for local port

# Architecture & Frameworks
...