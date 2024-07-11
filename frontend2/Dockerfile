# FROM node:lts as dependencies
# WORKDIR /event_web_application_front
# COPY . .
# COPY ./event_web_application_front/package.json ./event_web_application_front/
# COPY ./event_web_application_front/package-lock.json ./event_web_application_front/
# COPY ./event_web_application_front/next-env.d.ts ./event_web_application_front/
# COPY ./event_web_application_front/next.config.mjs ./event_web_application_front/
# COPY ./event_web_application_front/tsconfig.json ./event_web_application_front/
# COPY /frontend2/package.json /frontend2/
# COPY /frontend2/package-lock.json /frontend2
# COPY /frontend2/node_modules /frontend2/
# RUN npm install

# FROM node:lts as builder
# WORKDIR /frontend2/event_web_application_front
# COPY --from=dependencies ./event_web_application_front/node_modules ./node_modules
# COPY --from=dependencies ./event_web_application_front/public ./public
# COPY --from=dependencies ./event_web_application_front/.next ./.next
# COPY --from=dependencies ./event_web_application_front/node_modules ./app
# RUN npm run build

# FROM node:lts as runner
# WORKDIR /frontend2
# ENV NODE_ENV production

# EXPOSE 3000

# #CMD ["cd", "/frontend2/event_web_application_front/"]
# CMD ["npm", "run", "dev"]

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