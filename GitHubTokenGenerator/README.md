# GitHubTokenGenerator
A Simple cli for generating GitHub access tokens

Usage:

* `get-app-token`: Generate JWT access token for app

    * `--privatekeypath`    Required. Path to the app private key pem file

    * `--appid`             Required. The GitHub App ID

* `get-installation-token`: Generate an access token for an app installation

    * `--privatekeypath`    Required. Path to the app private key pem file

    * `--appid`             Required. The GitHub App ID

    * `--installationid`    Required. The app installation id for which to generate a token
