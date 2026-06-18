# Stage 1: Build
FROM node:22-alpine AS builder

WORKDIR /app

# Copy package files
COPY package*.json ./
COPY .npmrc ./

# Install dependencies
RUN npm ci --verbose 2>&1 | head -100 || npm install --legacy-peer-deps

# Copy application files
COPY . .

# Set environment variables for build
ARG API_URL=http://localhost:5000/api
ENV API_URL=$API_URL
ENV NODE_OPTIONS="--max-old-space-size=2048"

# Build the application
RUN npm run build -- --configuration development

# Stage 2: Runtime (Nginx)
FROM nginx:alpine

COPY nginx.conf /etc/nginx/nginx.conf

COPY --from=builder /app/dist/word-frequency-ui/browser /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
