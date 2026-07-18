# Ruqi Store — System Analysis and Design Documentation

**Project:** Ruqi Store (Full-Stack E-Commerce Furniture Application)  
**Prepared by:** Business & Information Technology Portfolio Project  
**Date:** July 2026  
**Version:** 1.0  

---

## Document Purpose

This document serves as a **complete system analysis and design deliverable** for the **Ruqi Store** ecosystem. It demonstrates the end-to-end process of analyzing e-commerce business workflows, gathering functional and non-functional requirements, modeling database and system behaviors, and designing a robust, modern technical solution. 

The report covers every phase of the Software Development Lifecycle (SDLC) from initial planning and stakeholder analysis, through object-oriented design, to detailed architectural blueprints.

---

## Table of Contents

| #  | Section | Description | Page Est. |
| :--- | :--- | :--- | :--- |
| 1  | [01. Project Introduction](./01-introduction.md) | Project overview, scope, objectives, and business goals. | 1 |
| 2  | [02. Stakeholder & User Role Analysis](./02-stakeholder-analysis.md) | Who the system serves: roles (Customer, Manager, Admin). | 1 |
| 3  | [03. System Requirements Specification](./03-requirements.md) | Fully documented functional and non-functional requirements. | 3 |
| 4  | [04. Use Case Model & Descriptions](./04-use-case-model.md) | Core system actors, unified use case diagrams, and flows. | 4 |
| 5  | [05. User Stories & Acceptance Criteria](./05-user-stories.md) | Agile stories mapped with formal acceptance criteria. | 2 |
| 6  | [06. Domain Model & Object Structure](./06-domain-model.md) | Conceptual schema, entities, and static business relationships. | 3 |
| 7  | [07. UML Behavioral Diagrams](./07-uml-behavioral.md) | Sequence diagrams mapping checkout and inventory synchronization. | 3 |
| 8  | [08. Database Design & Schema](./08-database-design.md) | ER diagrams, physical tables, keys, and constraints. | 3 |
| 9  | [09. Architectural Overview](./09-architecture.md) | N-Tier layered architecture style and request lifecycles. | 2 |
| 10 | [10. Detailed Component Design](./10-detailed-design.md) | Service interfaces, API contracts, and business validations. | 2 |
| 11 | [11. UI/UX Design](./11-ui-ux-design.md) | Page specifications, responsive layouts, localization, and RTL. | 2 |
| 12 | [12. Requirements Traceability Matrix](./12-traceability.md) | End-to-end mapping of requirements to design code. | 1 |
| 13 | [13. Project Appendices](./13-appendices.md) | Glossary, references, technology stack, and guidelines. | 1 |

**Total Estimated Length:** ~28 pages

---

## How to Read This Document

Each documentation section is systematically mapped directly to academic software analysis and development course chapters:

| Document Section | Theoretical Chapter Link |
| :--- | :--- |
| **01. Introduction** | Ch 1 — SDLC & Project Planning |
| **02. Stakeholder Analysis** | Ch 2 — Requirements Engineering & Gathering |
| **03. Requirements Specification** | Ch 2 — Requirements Engineering & Specifications |
| **04. Use Case Model** | Ch 3 — Behavioral Modeling & Use Cases |
| **05. User Stories** | Ch 3 — User Stories & Agile Backlog Management |
| **06. Domain Model** | Ch 4 — Object-Oriented Analysis / Ch 5 — UML |
| **07. UML Behavioral Diagrams** | Ch 5 — Dynamic System Modeling with UML |
| **08. Database Design** | Ch 6 — Relational Database Design & Normalization |
| **09. Architectural Overview** | Ch 7 — System & Architectural Patterns |
| **10. Detailed Design** | Ch 8 — Class Design & API Contracts |
| **11. UI/UX Design** | Ch 9 — Human-Computer Interaction & Accessibility |
| **12. Traceability Matrix** | Ch 10 — Project Handover & Quality Assurance |

---

## Document Conventions

In order to maintain strict engineering standards across all documentation files, the following identifiers and tools are utilized:

* **`FR-XXX`** — Functional Requirement identifier (e.g., `FR-01`).
* **`NFR-XXX`** — Non-Functional Requirement identifier (e.g., `NFR-U1`).
* **`UC-XXX`** — Use Case identifier (e.g., `UC-03`).
* **`US-XXX`** — User Story identifier (e.g., `US-02`).
* **Diagram Notation:** All structural and behavioral diagrams (ERD, Sequence Flows) are written using **Mermaid.js** notation for seamless rendering directly within GitHub Markdown.
* **Priority Levels:** Managed strictly via the **MoSCoW** prioritization matrix (*Must-Have / Should-Have / Could-Have / Won't-Have*).
* **Technology Implementation:** Designed for a robust implementation using **ASP.NET Core MVC** with Entity Framework Core and SQL Server.
