#!/usr/bin/env sh

# abort on errors
set -e

# build
npm run docs:build

# navigate into the build output directory
cd docs/.vitepress/dist

# who am I?
git config --global user.email "emersonbottero@gmail.com"
git config --global user.name "Emerson Bottero Branco"

git init
git add -A
git commit -m 'deploy'
# if you are deploying to https://<USERNAME>.github.io/<REPO>
git push -f git@github.com:emersonbottero/ServiceNow.Core.git master:gh-pages

cd -