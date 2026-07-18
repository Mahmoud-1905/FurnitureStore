# 09. Architectural Design

## 9.1 Architecture Pattern: Three-Tier

Ruqi Store uses a **three-tier (layered) architecture**, separating the system into Presentation, Business Logic, and Data tiers. This pattern guarantees clean separation of concerns, allows independent scaling of individual tiers, and matches the requirements of a high-performance e-commerce catalog and transaction system.

```mermaid
graph TB

    subgraph Presentation["Presentation Tier (Client)"]
        Browser["Web/Mobile Browser"]
        React["React.js SPA / Client UI"]
    end

    subgraph Logic["Business Logic Tier (Server)"]
        API["REST API - Node.js / Express"]
        Auth["Authentication Middleware - JWT"]
        BL["Business Services & Workflows"]
        Val["Validation & Policy Layer"]
    end

    subgraph Data["Data Tier"]
        DB[("SQL Server Database")]
        FileStore[("File Storage - AWS S3")]
        Cache[("Redis Cache")]
    end

    subgraph External["External Services"]
        SMTP["Email Service - SendGrid"]
        Pay["Payment Gateway - Stripe"]
    end

    Browser --> React
    React -->|HTTPS / REST API| API
    API --> Auth
    Auth --> BL
    BL --> Val
    Val --> DB
    BL --> FileStore
    BL --> Cache
    BL --> SMTP
    BL --> Pay

    style Presentation fill:#e3f2fd,stroke:#1e88e5,stroke-width:2px
    style Logic fill:#fff3e0,stroke:#f57c00,stroke-width:2px
    style Data fill:#e8f5e9,stroke:#43a047,stroke-width:2px
    style External fill:#fce4ec,stroke:#d81b60,stroke-width:2px
```
## 9.2 Technology Stack

| Layer | Technology | Justification |
| :--- | :--- | :--- |
| Frontend | React.js | Component-based structure, fast virtual DOM rendering, and a robust ecosystem for dynamic shopping carts. |
| Backend | Node.js + Express | Event-driven, asynchronous I/O that handles concurrent customer browsing and checkout operations efficiently. |
| Database | SQL Server (MSSQL) | Strict relational model, robust transaction handling (ACID) for order payments, and deep referential integrity. |
| Cache | Redis | Temporary session storage, caching frequently accessed catalog products, and maintaining current cart states. |
| File Storage | AWS S3 | Highly scalable and secure storage for product images, marketing assets, and invoice PDFs. |
| Authentication | JWT + bcrypt | Stateless secure authorization for active sessions and industry-standard salted password hashing. |
| API Style | RESTful | Clean resource-oriented HTTP routing, easy integration, and well-supported testing suites. |
| External Integrations | SendGrid & Stripe | Reliable transactional email notifications and secure, PCI-compliant payment card processing. |
## 9.3 Component Diagram

This diagram displays the structural components of Ruqi Store and how requests propagate from public UI components down to database repositories.

```mermaid
graph LR

    subgraph Frontend["Frontend Client (React)"]
        Login["Login / Profile"]
        Home["Catalog Home"]
        CartUI["Shopping Cart View"]
        CheckoutUI["Checkout Page"]
        ShowroomUI["Showroom Scheduler"]
        WishlistUI["Wishlist View"]
    end

    subgraph Backend["Backend API Controllers"]
        AuthC["Auth Controller"]
        ProdC["Product Controller"]
        CartC["Cart Controller"]
        OrdC["Order Controller"]
        ShowC["Showroom Controller"]
    end

    subgraph Services["Business Logic Services"]
        AuthS["Identity Service"]
        StockS["Inventory Service"]
        CartS["Cart Service"]
        OrderS["Order Processing Service"]
        BookS["Appointment Service"]
        MailS["Notification Service"]
        PayS["Payment Integration Service"]
        WishlistS["Wishlist Service"]
        AuditS["Audit Logging Service"]
    end

    subgraph Data["Data Access Repositories"]
        UserR["User Repository"]
        ProdR["Product Repository"]
        CartR["Cart Repository"]
        OrderR["Order Repository"]
        BookR["Appointment Repository"]
        WishlistR["Wishlist Repository"]
        AuditR["Audit Log Repository"]
    end

    subgraph External["External Integrations"]
        SendGrid["SendGrid API"]
        Stripe["Stripe SDK"]
        S3[("AWS S3 Bucket")]
    end

    DB[("SQL Server Database")]

    Frontend -->|REST API Calls| Backend
    Backend --> Services
    Services --> Data

    MailS -->|SMTP| SendGrid
    PayS -->|HTTPS| Stripe
    ProdC -->|Upload / Retrieve Media| S3

    Data --> DB

    style Frontend fill:#e3f2fd,stroke:#1e88e5,stroke-width:2px
    style Backend fill:#fff3e0,stroke:#f57c00,stroke-width:2px
    style Services fill:#e8f5e9,stroke:#43a047,stroke-width:2px
    style Data fill:#f5f5f5,stroke:#9e9e9e,stroke-width:2px
    style External fill:#fce4ec,stroke:#d81b60,stroke-width:2px
```
## 9.4 Architectural Decisions

| Decision Topic | Selected Approach | Alternatives Considered | Rationale |
| :--- | :--- | :--- | :--- |
| Architecture Pattern | Three-Tier Monolith | Microservices | Monolith offers rapid deployment and low operational overhead for a mid-sized e-commerce store, avoiding network latency and complex transaction routing. |
| Frontend Rendering | Single Page App (SPA) | Server-Side Rendering (SSR) | SPA provides seamless state transitions, which is ideal for shopping carts and showroom calendars, while isolating UI processing from the server. |
| Database System | Relational Database (SQL Server) | NoSQL (MongoDB) | E-commerce checkout requires absolute transaction safety (ACID) to update stock levels and record payments securely without collisions. |
| Authentication Style | Stateless JWT Tokens | Session Cookies | JWT tokens support horizontal backend scaling without sticky-session overhead and integrate easily with future native mobile applications. |
| File Hosting | External Storage (AWS S3) | Local Disk Storage | Offloads heavy asset delivery, including high-resolution product and showroom media, from the application server, reducing disk load and improving response times. |

## 9.5 Deployment View

This physical blueprint displays the production network topology of the Ruqi Store system.

```mermaid
graph TB

    subgraph Client["Client Tier"]
        PC["Desktop Browser"]
        Tablet["Tablet Browser"]
        Mobile["Mobile Device"]
    end

    subgraph CDN["Content Delivery Network"]
        CloudFront["AWS CloudFront CDN"]
    end

    subgraph WebServer["Application Server Layer"]
        LB["AWS Application Load Balancer"]
        App1["Node.js Instance 1"]
        App2["Node.js Instance 2"]
    end

    subgraph DataStores["Data Storage Layer"]
        DB_Primary[("SQL Server Primary")]
        DB_Replica[("SQL Server Replica - Read Only")]
        Redis[("Redis Cluster")]
        S3[("AWS S3 Bucket")]
    end

    PC -->|HTTPS Request| CloudFront
    Tablet -->|HTTPS Request| CloudFront
    Mobile -->|HTTPS Request| CloudFront

    CloudFront -->|Static Media & Assets| S3
    CloudFront -->|Dynamic API Requests| LB

    LB --> App1
    LB --> App2

    App1 --> DB_Primary
    App2 --> DB_Primary

    DB_Primary -.->|Asynchronous Replication| DB_Replica

    App1 --> Redis
    App2 --> Redis

    App1 --> S3
    App2 --> S3

    style Client fill:#e3f2fd,stroke:#1e88e5,stroke-width:2px
    style CDN fill:#fce4ec,stroke:#d81b60,stroke-width:2px
    style WebServer fill:#fff3e0,stroke:#f57c00,stroke-width:2px
    style DataStores fill:#e8f5e9,stroke:#43a047,stroke-width:2px
```

Our system architecture is designed to handle up to 1,000 concurrent shopping sessions with ease. The stateless application layer allows developers to add more virtual server instances (App Instance 3, 4, etc.) behind the Application Load Balancer to scale capacity horizontally whenever seasonal traffic peaks.

---

[← Previous: Database Design](./08-database-design.md) | [Back to Index](./00-index.md) | [Next: Detailed Design →](./10-detailed-design.md)
