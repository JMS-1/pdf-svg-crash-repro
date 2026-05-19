# Reproduce

Download approtiate docker image from [GIT tag](https://github.com/JMS-1/pdf-svg-crash-repro/tags), e.g. [4.8.0](https://github.com/JMS-1/pdf-svg-crash-repro/releases/download/4.8.0/badsvg-4.8.0.tar).

Our reproduction system is a PI5 docker host.

```
PRETTY_NAME="Debian GNU/Linux 12 (bookworm)"
NAME="Debian GNU/Linux"
VERSION_ID="12"
VERSION="12 (bookworm)"
VERSION_CODENAME=bookworm
ID=debian
HOME_URL="https://www.debian.org/"
SUPPORT_URL="https://www.debian.org/support"
BUG_REPORT_URL="https://bugs.debian.org/"
```

```
https://github.com/JMS-1/pdf-svg-crash-repro/releases/download/4.8.0/badsvg-4.8.0.tar
```

Unpack the image and run, e.g. for 4.8.0:

```
docker load -i badsvg-4.8.0.tar
docker run --name badsvg badsvg:latest
```

If all is fine it outputs `OK` and a PDF is generated which could be [inspected](expected.pdf):

```
docker exec badsvg ls -l
docker cp badsvg:/app/some.pdf /tmp/some.pdf
```

If it crashes a `core` dump is created as with 4.9.0:

```
docker load -i badsvg-4.9.0.tar
docker run --name badsvg badsvg:latest
```

To create the docker image checkout the correct branch and just run `./pubit` - docker build environment required, evtl. cross build features enabled.
