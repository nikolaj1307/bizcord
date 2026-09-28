CREATE TABLE notifications
(
    id         TEXT PRIMARY KEY,
    title      TEXT NOT NULL,
    message    TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
)

