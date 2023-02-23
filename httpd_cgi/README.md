`chmod +x test.cgi`

`ln -s /home/nikolai/code/stupidtests/httpd_cgi/test.cgi /tmp/cgi-server/cgi-bin/test.cgi`

`sudo busybox httpd -p 9696 -f -v -h /tmp/cgi-server`

`curl http://localhost:9696/cgi-bin/test.cgi`

` curl --data-ascii @temp.json http://localhost:9696/cgi-bin/echo.cgi`
