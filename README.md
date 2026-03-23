# EasyDirectCMD

EasyDirectCMD is a lightweight command-line tool that indexes, searches, and opens files quickly across a system.

<img width="784" height="274" alt="image" src="https://github.com/user-attachments/assets/8d23e355-b640-467a-8981-24e7db69753a" />

##  Purpose

Windows provides file search through its built-in indexing service. However, this project was designed to:

- Provide a developer-controlled file indexing system
- Enable faster and more predictable search behaviour
- Allow customization of search logic
- Operate independently of the Windows Search service

Instead of relying on the Windows indexing service, this application builds and manages its own file index using a custom data structure and local storage.

---

##  How It Works

### Indexing
- Scans all available drives
- Recursively reads files
- Stores results in:
  - In-memory dictionary
  - Local file (`EasyDirectCMD_Location_Index.txt`)

### Data Structure

<img width="4096" height="2230" alt="image" src="https://github.com/user-attachments/assets/9538c1c6-9cc2-493f-b9fe-52d4c6cbc5d1" />

- Key → File name  
- Value → List of full file paths  

This structure enables:
- Fast exact lookups
- Support for duplicate file names across different directories

---

##  Features

- Fast file search (exact & partial match)
- Independent indexing system (not reliant on Windows Search)
- Open files directly from the terminal
- Command-based interface
- Persistent index storage

---

##  Commands

| Command        | Description                  |
|----------------|------------------------------|
| search / s     | Search for files             |
| sa             | Search for applications      |
| cmd <path>     | Open file/application        |
| build          | Build file index             |
| clear / clr    | Clear console                |
| exit / quit    | Exit application             |
| help / ?       | Show help menu               |

---

## Design Considerations

### Windows Search vs Custom Indexing

Windows stores indexed file data using its internal indexing service. While effective, it has limitations:

- Limited control over indexing behaviour
- Dependency on system services
- Less flexibility for custom search logic

This application replaces that with:
- A custom indexing engine
- Full control over how files are indexed and retrieved
- Simplified and predictable search behaviour

---

##  Limitations

- Initial indexing can be slow due to full system scan
- Higher memory usage from in-memory storage
- Indexing process is currently synchronous (blocking)

---

##  Improvements

- Add asynchronous indexing to prevent blocking
- Implement file type filtering (e.g. .exe, .pdf)
- Replace in-memory storage with a database (e.g. SQLite)
- Improve search performance and ranking
- Add multi-threaded indexing
