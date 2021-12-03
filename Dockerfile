# FROM nginx:alpine
# COPY /dist/spa/ /usr/share/nginx/html
# EXPOSE 80

FROM nginx:latest

EXPOSE 80

#COPY nginx.conf /etc/nginx/nginx.conf

COPY /dist/spa/ /var/www
#COPY /node_modules /var/www/node_modules

CMD ["nginx"]