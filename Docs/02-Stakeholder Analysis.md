# 2. Stakeholder Analysis

## 2.1 Stakeholder Register

| # | Stakeholder | Role | Interest | Influence | Key Concern |
|---|-------------|------|----------|-----------|-------------|
| 1 | Project Supervisor | Academic Oversight | High | High | Technical standards, documentation, and deadlines |
| 2 | Admin | Platform Governance | High | High | User management, sales analytics, and system security |
| 3 | Sellers (Furniture Vendors) | Inventory & Sales | High | Medium | Product display, stock management, and profit tracking |
| 4 | Customers | End Users | High | Low | Quality of 3D/AR previews, payment security, and UX |
| 5 | Delivery Personnel | Logistics & Shipping | Medium | Low | Accurate GPS locations, delivery schedules, and updates |
| 6 | Development Team | Technical Implementation | High | High | Scalability, performance, and clean code architecture |

## 2.2 Stakeholder Map

```mermaid
quadrantChart
    title Stakeholder Influence vs. Interest
    x-axis Low Interest --> High Interest
    y-axis Low Influence --> High Influence
    quadrant-1 Manage Closely
    quadrant-2 Keep Satisfied
    quadrant-3 Monitor
    quadrant-4 Keep Informed
    "Project Supervisor": [0.90, 0.95]
    "Admin": [0.85, 0.90]
    "Development Team": [0.80, 0.85]
    "Sellers": [0.75, 0.60]
    "Customers": [0.70, 0.35]
    "Delivery Personnel": [0.55, 0.30]

   ```

## 2.3 Stakeholder Needs Summary

**Project Supervisor** — Needs to ensure that the project structure and objectives adhere to the required academic standards and are delivered on time.


**Administrators** — Need guidance on user management, monitoring system activity, and generating reports through their dashboard.

**Sellers/Vendors** — Need to understand how to manage furniture products, process orders, and interact with customers through the platform.

**End Users (Customers)** — Need to know how to navigate the system, register, order furniture, and easily track their shipments.

**Delivery Personnel** — Need to access delivery tasks, use address data, and update order status (Shipped/Delivered).

**Development Team** — Need to understand the project scope and system design to serve as a reference for programming and testing.

---
[← Back to Introduction](Introduction.md) | [Next: Requirements Analysis →](./03-Requirements-analysis.md)
