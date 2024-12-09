# Requirements Document - ParkFlow: Parking Management System

## **Project Description**
The purpose of this project is to develop a system to manage vehicle entry and exit in a parking lot. The system will record vehicle details, track entry and exit times, and generate usage reports.

---

## **Functional Requirements**
### **Vehicle Registration**
- The system must allow vehicle registration with the following details:
  - **License Plate:** Unique identifier for the vehicle.
  - **Brand:** Vehicle manufacturer (e.g., Toyota, Ford).
  - **Model:** Vehicle model (e.g., Corolla, Fiesta).
  - **Color:** Main color of the vehicle.

### **Entry and Exit Control**
- The system must record:
  - **Entry Time:** Date and time when the vehicle enters the parking lot.
  - **Exit Time:** Date and time when the vehicle leaves the parking lot.
  - **Status:** Indicates whether the vehicle is currently parked.

### **Report Generation**
- The system must generate detailed reports containing:
  - A list of currently parked vehicles with entry times.
  - A history of exited vehicles, including entry time, exit time, and duration of stay.
  - Daily summaries of parking activity, including:
    - Total vehicles entered.
    - Total vehicles exited.
    - Average parking duration.

---

## **Non-Functional Requirements**
- **User-Friendly Interface:** The system must have an intuitive interface for quick access to core functionalities.
- **Performance:** The system must handle up to 500 vehicle records simultaneously without performance degradation.
- **Data Persistence:** All data must be securely stored in a local database (e.g., SQLite or MySQL).
- **Portability:** The system must be compatible with Windows operating systems.

---

## **Technical Requirements**
- **Programming Language:** C# with Windows Forms or ASP.NET.
- **Database:** SQLite (or another lightweight relational database).
- **Reports:** Use libraries for generating PDF reports or integrate with Excel.

---

## **System Workflow**
1. **Vehicle Registration:**
   - The operator registers the vehicle details.
2. **Vehicle Entry:**
   - The operator selects a registered vehicle or adds a new one and records the entry time.
3. **Vehicle Exit:**
   - The operator searches for the vehicle by license plate and records the exit time.
4. **View Parked Vehicles:**
   - The system displays a list of vehicles currently parked.
5. **Generate Reports:**
   - The operator generates daily, weekly, or monthly reports.

---

## **Business Rules**
- Only registered vehicles can be logged at the entry gate.
- The system must prevent duplicate entry logs (a vehicle cannot re-enter without an exit record).
- Reports must organize information chronologically by date and time.

---

## **Mockups**
Include images or diagrams illustrating the system's interface.

---

## **Roadmap**
1. Set up the development environment.
2. Implement the database structure.
3. Develop the vehicle registration interface.
4. Build entry and exit control functionalities.
5. Implement report generation features.
6. Conduct testing and validation.

---

## **Contact**
For questions or suggestions, contact us at:
- **Email:** felipe.fends@gmail.com
