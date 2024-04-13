# Use Node.js v21.6.2 as the base image for build stage
FROM node:21.6.2-alpine as build

# Set working directory
WORKDIR /frontend

# Copy package.json and package-lock.json
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application
COPY . .

# Build the React app
RUN npm run build

# Use Caddy to serve the built React app
FROM caddy:alpine

# Copy built React app from build stage
COPY --from=build /frontend/build /srv

# Expose port 85
EXPOSE 85

# Start Caddy
CMD ["caddy", "file-server", "--root", "/srv", "--listen", "0.0.0.0:85"]
