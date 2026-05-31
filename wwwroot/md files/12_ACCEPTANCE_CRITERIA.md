# 12_ACCEPTANCE_CRITERIA

## Functional Acceptance

- **Catalog Browsing**: Users can search, filter, and view product details without errors.
- **Cart & Checkout**: Items can be added to the cart, persisted across sessions, and purchased. Inventory is decremented atomically.
- **Appointment Booking**: Customers can book showroom appointments up to 24 h in advance; manager receives notification; cancellation respects the 24 h rule.
- **User Roles**: Access control matches the matrix in **03_USER_ROLES_AND_PERMISSIONS.md** (Guest, Customer, Store Manager, Administrator).
- **Reviews**: Reviews are only accepted for delivered orders and are stored with `IsVerified = true`.
- **Admin Functions**: Administrators can activate/deactivate users, assign roles, and view audit logs.

## Non‑Functional Acceptance

- **Performance**: API 95th‑percentile response < 500 ms; page render < 2 s on typical broadband.
- **Security**: JWT signed with strong secret, password hashing with bcrypt cost 12, role‑based authorization enforced on all protected endpoints.
- **Reliability**: System uptime ≥ 99.5 % (tested via simulated failure scenarios).
- **Usability**: UI complies with WCAG 2.1 AA, supports RTL Arabic and LTR English, responsive across desktop/tablet.
- **Scalability**: Stateless API with Redis caching, capable of handling 200 RPS in load tests.

## Documentation Acceptance

- All markdown files listed in the documentation set are present, correctly named, and free of line‑number prefixes.
- The **Implementation Plan** (11) and **Acceptance Criteria** (12) are included and up‑to‑date.
- Diagrams and placeholder images are referenced with proper file paths.

## Final Checklist

- [ ] All unit, integration, and UI tests pass (`npm test && dotnet test`).
- [ ] Load testing report meets performance thresholds.
- [ ] Security scan (OWASP ZAP) shows no critical issues.
- [ ] Deployment to staging completes without errors.
- [ ] Documentation reviewed by the supervisor and signed off.

---

*This acceptance criteria file is ready for inclusion in the repository.*
