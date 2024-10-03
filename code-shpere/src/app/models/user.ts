export interface User {
    id: number;               // Unique identifier for the user
    username: string;        // Username of the user
    email: string;           // Email address of the user
    password?: string;       // Password (optional, do not include in responses for security)
    firstName: string;       // User's first name
    lastName: string;        // User's last name
    role?: string;           // Role of the user (e.g., "Freelancer", "Client")
    createdAt?: Date;        // Date the user account was created
    updatedAt?: Date;        // Date the user account was last updated
  }
  