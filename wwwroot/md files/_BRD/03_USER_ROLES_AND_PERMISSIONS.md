# 03_USER_ROLES_AND_PERMISSIONS

📌 **Defines what each role CAN and CANNOT do. Drives role-based authorization.**

> 💬 *"Every role is an access contract. Cannot Do is as important as Can Do."*

---

## 1. Admin
*   **Can Do:** Full access to system configuration, user management, and all clinic data.
*   **Cannot Do:** Act as a medical provider (e.g., write prescriptions), book personal appointments through the admin interface.

## 2. Doctor
*   **Can Do:** View and manage own appointments, access medical records for assigned patients.
*   **Cannot Do:** Manage other doctors' schedules, alter system configurations, or access unassigned patient records.

## 3. Patient
*   **Can Do:** Register an account, book appointments, and view own medical history/records.
*   **Cannot Do:** View other patients' records, access clinic schedules, or modify appointment availability.

---

## 4. Role Matrix

| Action | Admin | Doctor | Patient |
| :--- | :---: | :---: | :---: |
| **1. Manage System Users** | ✅ | ❌ | ❌ |
| **2. View All Appointments** | ✅ | ❌ | ❌ |
| **3. Manage Own Appointments** | ❌ | ✅ | ❌ |
| **4. Book an Appointment** | ❌ | ❌ | ✅ |
| **5. View Own Records** | ❌ | ❌ | ✅ |
| **6. View Patient Records** | ✅ | ✅ (Assigned) | ❌ |
| **7. System Configuration** | ✅ | ❌ | ❌ |

---

⚠️ **Mistake to Avoid:** Forgetting the 'Cannot Do' list — creates security holes in the application.

---

### ASP.NET Core Authorization Plan
*(Based on the Antigravity Prompt: Plan the ASP.NET Core authorization: which controllers need `[Authorize(Roles=...)]`)*

*   `[Authorize(Roles = "Admin")]` 
    *   `AdminController`
    *   `UsersController`
*   `[Authorize(Roles = "Admin,Doctor")]`
    *   `MedicalRecordsController` (with resource-based authorization for doctors)
*   `[Authorize(Roles = "Doctor")]`
    *   `DoctorScheduleController`
*   `[Authorize(Roles = "Patient")]`
    *   `PatientAppointmentsController`
