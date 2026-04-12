-- Скрипт миграции
-- 2026-03-04

alter table "public"."companies" add column load_options jsonb;
