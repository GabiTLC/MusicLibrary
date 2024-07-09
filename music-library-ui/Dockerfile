# Stage 1: Build the Angular app
FROM node:18 as build
WORKDIR /app
COPY package.json package-lock.json ./

RUN npm install -g @angular/cli

RUN npm install
COPY . .
# RUN npm run build --prod

EXPOSE 4200

CMD ["ng", "serve", "--host", "0.0.0.0"]

# # Stage 2: Serve the Angular app with nginx
# FROM nginx:alpine
# COPY --from=build /app/dist/music-library-ui /usr/share/nginx/html
# COPY ./nginx.conf  /etc/nginx/conf.d/default.conf

# EXPOSE 80
# CMD ["nginx", "-g", "daemon off;"]
