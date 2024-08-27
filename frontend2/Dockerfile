FROM node:latest as dependencies
WORKDIR /event_web_application_front
COPY . .
COPY package.json package-lock.json ./
RUN npm install --frozen-lockfile

FROM node:latest as builder
WORKDIR /event_web_application_front
COPY --from=dependencies /event_web_application_front/node_modules ./node_modules
RUN npm run build /event_web_application_front/

FROM node:latest as runner
WORKDIR /event_web_application_front
COPY /event_web_application_front/public ./public
COPY /event_web_application_front/app ./app
COPY /event_web_application_front/package.json ./package.json
COPY /event_web_application_front/.next ./.next
COPY /event_web_application_front/node_modules ./node_modules
RUN npm install next
RUN npm install axios
RUN npm install bootstrap

EXPOSE 3000
CMD ["npm","run","build"]
CMD ["npm","run", "dev"]