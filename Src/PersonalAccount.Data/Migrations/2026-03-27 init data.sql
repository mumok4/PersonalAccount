-- Скрипт миграции
-- 2026-03-27
alter table companies add name text;

insert into companies(id, name, inn, address, load_options)
values('14e54725-0efc-42b8-a27d-a84f9a7257c5','TEST', '1234567890', 'г Москва, ул Тестовая, д 1', '{}');