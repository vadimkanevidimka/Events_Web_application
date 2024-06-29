FROM node:lts as dependencies
WORKDIR /event_web_application_front
COPY /event_web_application_front/package.json ./
COPY /event_web_application_front/package-lock.json ./
COPY /event_web_application_front/next-env.d.ts ./
COPY /event_web_application_front/next.config.mjs ./
COPY /event_web_application_front/tsconfig.json ./
COPY /node_modules /node_modules
RUN npm install

FROM node:lts as builder
WORKDIR /event_web_application_front
COPY --from=dependencies /event_web_application_front/node_modules ./node_modules
COPY --from=dependencies /event_web_application_front/public ./public
COPY --from=dependencies /event_web_application_front/.next ./.next
COPY --from=dependencies /event_web_application_front/node_modules ./app
RUN npm run build

FROM node:lts as runner
WORKDIR /event_web_application_front
ENV NODE_ENV production

EXPOSE 3000
RUN npm run dev