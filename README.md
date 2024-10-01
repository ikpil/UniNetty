# UniNetty Project

*UniNetty is a port of [Azure/DotNetty](https://github.com/Azure/DotNetty), an asynchronous event-driven network framework designed for developing high-performance, maintainable protocol servers and clients, fully compatible with Unity3D.*  

*If you'd like to support the project, we'd appreciate starring(⭐) our repos on Github for more visibility.*

---

![GitHub License](https://img.shields.io/github/license/ikpil/UniNetty?style=for-the-badge)
![Languages](https://img.shields.io/github/languages/top/ikpil/UniNetty?style=for-the-badge)
![GitHub repo size](https://img.shields.io/github/repo-size/ikpil/UniNetty?style=for-the-badge)
[![GitHub Repo stars](https://img.shields.io/github/stars/ikpil/UniNetty?style=for-the-badge&logo=github)](https://github.com/ikpil/UniNetty)
[![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/ikpil/UniNetty/dotnet.yml?style=for-the-badge&logo=github)](https://github.com/ikpil/UniNetty/actions/workflows/dotnet.yml)
[![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/ikpil/UniNetty/codeql.yml?style=for-the-badge&logo=github&label=CODEQL)](https://github.com/ikpil/UniNetty/actions/workflows/codeql.yml)
[![GitHub commit activity](https://img.shields.io/github/commit-activity/m/ikpil/UniNetty?style=for-the-badge&logo=github)](https://github.com/ikpil/UniNetty/commits)
[![GitHub issues](https://img.shields.io/github/issues-raw/ikpil/UniNetty?style=for-the-badge&logo=github&color=44cc11)](https://github.com/ikpil/UniNetty/issues)
[![GitHub closed issues](https://img.shields.io/github/issues-closed-raw/ikpil/UniNetty?style=for-the-badge&logo=github&color=a371f7)](https://github.com/ikpil/UniNetty/issues)
[![GitHub Sponsors](https://img.shields.io/github/sponsors/ikpil?style=for-the-badge&logo=GitHub-Sponsors&link=https%3A%2F%2Fgithub.com%2Fsponsors%2Fikpil)](https://github.com/sponsors/ikpil)

---


## 🚀 Features

- **Unity3D Compatibility** : Integrate DotNetty's functionality into Unity projects, simplifying the implementation of networking features.
- **Unified API for various transport types** : blocking and non-blocking socket
- **Flexible Event Model** : Based on a flexible and extensible event model which allows clear separation of concerns
- **Highly Customizable Thread Model** : single thread, one or more thread pools such as SEDA
- **Datagram Socket** : True connectionless datagram socket support

UniNetty is divided into multiple modules, each contained in its own folder:

- [UniNetty.Logging](https://github.com/ikpil/UniNetty/tree/main/src/UniNetty.Logging) : Logging framework used internally by UniNetty
- [UniNetty.Common](https://github.com/ikpil/UniNetty/tree/main/src/UniNetty.Common) : Provides utility functions and common components for asynchronous and concurrent operations.
- [UniNetty.Buffers](https://github.com/ikpil/UniNetty/tree/main/src/UniNetty.Buffers) : Manages efficient memory allocation and deallocation for network data buffers.
- [UniNetty.Transport](https://github.com/ikpil/UniNetty/tree/main/src/UniNetty.Transport) : Implements core TCP and UDP transport functionalities with scalable event loop support.
- [UniNetty.Codecs](https://github.com/ikpil/UniNetty/tree/main/src/UniNetty.Codecs) : Handles encoding and decoding of data across network channels.
- [UniNetty.Codecs.Http](https://github.com/ikpil/UniNetty/tree/main/src/UniNetty.Codecs.Http) : Supports HTTP/1.1 and handles full HTTP request/response lifecycle.
- [UniNetty.Codecs.Mqtt](https://github.com/ikpil/UniNetty/tree/main/src/UniNetty.Codecs.Mqtt) : Implements MQTT protocol for IoT communication with efficient message handling.
- [UniNetty.Codecs.Protobuf](https://github.com/ikpil/UniNetty/tree/main/src/UniNetty.Codecs.Protobuf) : Facilitates Protocol Buffers serialization and deserialization for compact binary communication.
- [UniNetty.Codecs.Redis](https://github.com/ikpil/UniNetty/tree/main/src/UniNetty.Codecs.Redis) : Implements Redis protocol for building Redis-compatible servers or clients.
- [UniNetty.Handlers](https://github.com/ikpil/UniNetty/tree/main/src/UniNetty.Handlers) : Includes handlers for SSL/TLS, WebSocket, and other essential network features.
- [Tests](https://github.com/ikpil/UniNetty/tree/main/test) : Unit tests
- [Examples](https://github.com/ikpil/UniNetty/tree/main/examples) : Examples of usage

## ⚡ Getting Started

... 

## ⚙ How it Works

...

## 📚 Documentation & Links

- UniNetty Links
  - [UniNetty/issues](https://github.com/ikpil/UniNetty/issues)
 
- Official Links
  - [DotNetty/issues](https://github.com/Azure/DotNetty/issues)

## 🅾 License

UniNetty is licensed under MIT license, see [LICENSE.txt](https://github.com/ikpil/UniNetty/tree/main/LICENSE) for more information.

## 📹 Demo Video

...

