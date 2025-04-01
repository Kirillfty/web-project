FROM node:latest AS builder
WORKDIR /app
COPY front/vue-project/package.json ./
RUN npm install
COPY front/vue-project/ .
RUN npm run build

FROM nginx:alpine
COPY --from=builder /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]