-- Создаем таблицу для плоского хранения транзакций
create table journal_rows
(
    id uuid not null primary key DEFAULT gen_random_uuid(),
    code bigint not null,
    type_code bigint not null,
    receipt_number bigint not null,
    period timestamp with time zone NOT NULL,
    quantity numeric(15,2),
    price numeric(15,2),
    discount numeric(15,2),
    emploee_name text,
    category_name text,
    nomenclature_name text
);

create index journal_rows_code_ix on journal_rows(code);