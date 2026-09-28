CREATE TABLE messages
(
    id         UUID PRIMARY KEY,
    channel_id UUID NOT NULL,
    author_id  UUID NOT NULL,
    content    TEXT NOT NULL,
    posted_at  TIMESTAMP WITH TIME ZONE,
)