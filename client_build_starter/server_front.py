#!/usr/bin/env python3

import http.server
import socketserver

handler = http.server.SimpleHTTPRequestHandler

with socketserver.TCPServer(("127.0.0.1", 9000), handler) as httpd:
    print("web сервер запущен")
    httpd.serve_forever()
