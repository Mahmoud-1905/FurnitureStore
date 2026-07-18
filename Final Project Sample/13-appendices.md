# 13. Appendices

## Appendix A: Glossary

| Term | Definition |
|------|-----------|
| Actor | An external entity (person or system) that interacts with the Ruqi Store system (e.g., Customer, Admin, Payment Gateway). |
| API | Application Programming Interface — a set of rules for how software components communicate (e.g., Stripe Payment API). |
| BCrypt | A password hashing algorithm designed to resist brute-force attacks, used for securing user credentials. |
| CRUD | Create, Read, Update, Delete — the four basic operations performed on product catalog and order data. |
| DTO | Data Transfer Object — a lightweight object used to pass formatted data securely between system layers. |
| ER Diagram | Entity-Relationship Diagram — a visual model of database tables (`Products`, `Orders`, `ShowroomAppointments`) and their relationships. |
| MoSCoW | Prioritization method: Must have, Should have, Could have, Won't have (used for platform roadmap). |
| NFR | Non-Functional Requirement — a quality attribute like site performance, checkout security, or responsive usability. |
| Price Snapshot | A persistent copy of a product's price saved in the order history table at checkout to isolate it from future catalog changes. |
| REST | Representational State Transfer — an architectural style for designing scalable and stateless web APIs. |
| RTL | Right-to-Left — structural layout adaptation required for Arabic text and user interface mirroring. |
| SKU | Stock Keeping Unit — a unique alphanumeric code assigned to each distinct furniture item and variant for inventory tracking. |
| SRS | Software Requirements Specification — a document describing the complete scope and specifications of the Ruqi Store system. |
| Three-Tier | An architecture pattern separating the presentation, business logic (Services), and data storage (SQL Server) layers. |
| UML | Unified Modeling Language — a standardized set of diagrams used for modeling the e-commerce software structure. |
| Use Case | A description of how a user achieves a specific goal (e.g., checkout, booking a showroom appointment). |
| WCAG | Web Content Accessibility Guidelines — international standards used to ensure the retail platform is digitally accessible. |

## Appendix B: References

| # | Reference | Used In |
|---|-----------|---------|
| 1 | IEEE 830-1998: Recommended Practice for SRS | Section 3 |
| 2 | UML 2.5 Specification (OMG) | Sections 4, 6, 7 |
| 3 | Bootstrap 5 RTL Integration Guidelines | Section 11 |
| 4 | WCAG 2.1 AA Guidelines (W3C Standard) | Section 11, 12 |
| 5 | "Clean Architecture" by Robert C. Martin | Sections 9, 10 |
| 6 | "Design Patterns: Elements of Reusable Object-Oriented Software" | Section 10 |
| 7 | ASP.NET Core MVC & Entity Framework Core Documentation (Microsoft) | Sections 8, 9 |

## Appendix C: Revision History

| Version | Date | Author | Changes |
|---------|------|--------|---------|
| 0.1 | May 10, 2026 | Analysis Team | Initial draft — requirements gathering and project scope (Sections 1–3) |
| 0.5 | May 24, 2026 | Analysis Team | Added e-commerce use cases, user stories, and catalog domain model |
| 0.8 | Jun 08, 2026 | Design Team | Added SQL Server schema, MVC architecture, and checkout sequences |
| 1.0 | Jun 22, 2026 | Full Team | Final publication — all sections finalized, verified, and formatted |

## Appendix D: Diagram Index

| # | Diagram | Type | Section |
|---|---------|------|---------|
| 1 | Stakeholder Map | Quadrant Chart | 2.2 |
| 2 | Actor Generalization | Class Diagram | 4.1 |
| 3 | Use Case Diagram | Use Case Diagram | 4.2 |
| 4 | User Story Map | Block Diagram | 5.3 |
| 5 | Domain Model / Class Diagram | Class Diagram | 6.2 |
| 6 | Enumeration Types (Order/Appointment Status) | Class Diagram | 6.4 |
| 7 | Secure Checkout Sequence | Sequence Diagram | 7.1 |
| 8 | Book Showroom Appointment Sequence | Sequence Diagram | 7.2 |
| 9 | Add to Cart & Checkout Activity | Activity Diagram | 7.3 |
| 10 | Order Fulfillment State Machine | State Diagram | 7.4 |
| 11 | Stock Validation Activity | Activity Diagram | 7.5 |
| 12 | Database Entity-Relationship Diagram | ER Diagram | 8.1 |
| 13 | Three-Tier System Architecture | Architecture Diagram | 9.1 |
| 14 | Component Diagram | Component Diagram | 9.3 |
| 15 | Azure/SmarterASP Deployment Diagram | Deployment Diagram | 9.5 |
| 16 | User Interface Navigation Flow | Flowchart | 11.2 |

---

[← Previous: Traceability Matrix](./12-traceability.md) | [Back to Index](./00-index.md)
