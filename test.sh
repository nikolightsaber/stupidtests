#!/bin/bash

CI_COMMIT_TAG="4.5-pre2"
echo "version="$CI_COMMIT_TAG

TAG_VERSION=$(echo "$CI_COMMIT_TAG" | sed -E "s/(^[0-9])\..*/\\1/")
echo "tag="$TAG_VERSION

# sed command failed
if [ "$TAG_VERSION" == "$CI_COMMIT_TAG" ]; then TAG_VERSION=5; fi

if [ "$TAG_VERSION" == 4 ]; then
  echo "Tag is V4";
else
  echo "Tag is V5";
fi
