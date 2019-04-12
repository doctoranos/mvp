CREATE TABLE form (
    id SERIAL PRIMARY KEY,
    title TEXT NOT NULL
);

CREATE TABLE question (
    id SERIAL PRIMARY KEY,
    form_id INTEGER,
    body TEXT NOT NULL,

    FOREIGN KEY (form_id) REFERENCES form (id)
);

CREATE TABLE completed_form (
    id SERIAL PRIMARY KEY,
    form_id INTEGER,
    body jsonb NOT NULL,
    created_at timestamp DEFAULT now(),

    FOREIGN KEY (form_id) REFERENCES form (id)
);

CREATE OR REPLACE FUNCTION insert_form(title TEXT, questions TEXT[]) 
RETURNS void AS $$ 
DECLARE
    form_id INTEGER;
    question TEXT;
BEGIN 

INSERT INTO form (title) VALUES (title);

form_id := lastval();

FOREACH question IN ARRAY questions
LOOP
    INSERT INTO question (form_id, body) VALUES
    (form_id, question)
    ON CONFLICT DO NOTHING;

END LOOP;

END $$ LANGUAGE plpgsql; 