// ── Timezones ────────────────────────────────────────────────────────────────
export const TIMEZONES = [
  { value: 'UTC-12',   label: 'UTC-12 — Baker Island, Howland Island' },
  { value: 'UTC-11',   label: 'UTC-11 — American Samoa, Niue' },
  { value: 'UTC-10',   label: 'UTC-10 — Hawaii, Cook Islands' },
  { value: 'UTC-9',    label: 'UTC-9 — Alaska' },
  { value: 'UTC-8',    label: 'UTC-8 — Los Angeles, Vancouver, Tijuana' },
  { value: 'UTC-7',    label: 'UTC-7 — Denver, Phoenix, Calgary' },
  { value: 'UTC-6',    label: 'UTC-6 — Chicago, Mexico City, Guatemala' },
  { value: 'UTC-5',    label: 'UTC-5 — New York, Toronto, Bogotá, Lima' },
  { value: 'UTC-4',    label: 'UTC-4 — Santiago, Caracas, Halifax' },
  { value: 'UTC-3',    label: 'UTC-3 — São Paulo, Buenos Aires, Montevideo' },
  { value: 'UTC-2',    label: 'UTC-2 — South Georgia Island' },
  { value: 'UTC-1',    label: 'UTC-1 — Azores, Cape Verde' },
  { value: 'UTC',      label: 'UTC — Reykjavik, Accra, Abidjan' },
  { value: 'UTC+1',    label: 'UTC+1 — Lisbon (Summer), London (Summer), Paris, Berlin, Madrid, Rome' },
  { value: 'UTC+2',    label: 'UTC+2 — Athens, Cairo, Johannesburg, Helsinki, Kyiv' },
  { value: 'UTC+3',    label: 'UTC+3 — Moscow, Istanbul, Riyadh, Nairobi' },
  { value: 'UTC+4',    label: 'UTC+4 — Dubai, Baku, Tbilisi' },
  { value: 'UTC+5',    label: 'UTC+5 — Karachi, Tashkent, Yekaterinburg' },
  { value: 'UTC+5:30', label: 'UTC+5:30 — Mumbai, New Delhi, Colombo' },
  { value: 'UTC+6',    label: 'UTC+6 — Dhaka, Almaty, Omsk' },
  { value: 'UTC+7',    label: 'UTC+7 — Bangkok, Jakarta, Ho Chi Minh City' },
  { value: 'UTC+8',    label: 'UTC+8 — Beijing, Singapore, Kuala Lumpur, Perth, Taipei' },
  { value: 'UTC+9',    label: 'UTC+9 — Tokyo, Seoul, Osaka' },
  { value: 'UTC+9:30', label: 'UTC+9:30 — Adelaide, Darwin' },
  { value: 'UTC+10',   label: 'UTC+10 — Sydney, Melbourne, Brisbane, Vladivostok' },
  { value: 'UTC+11',   label: 'UTC+11 — Solomon Islands, New Caledonia' },
  { value: 'UTC+12',   label: 'UTC+12 — Auckland, Fiji, Wellington' },
];

// ── Date Formats ─────────────────────────────────────────────────────────────
export const DATE_FORMATS = [
  { value: 'dd/MM/yyyy',       label: 'dd/MM/yyyy — 31/01/2025 (Europe, Portugal, Brazil)' },
  { value: 'MM/dd/yyyy',       label: 'MM/dd/yyyy — 01/31/2025 (United States)' },
  { value: 'yyyy-MM-dd',       label: 'yyyy-MM-dd — 2025-01-31 (ISO 8601, International)' },
  { value: 'dd-MM-yyyy',       label: 'dd-MM-yyyy — 31-01-2025' },
  { value: 'dd.MM.yyyy',       label: 'dd.MM.yyyy — 31.01.2025 (Germany, Russia)' },
  { value: 'MM-dd-yyyy',       label: 'MM-dd-yyyy — 01-31-2025' },
  { value: 'd MMM yyyy',       label: 'd MMM yyyy — 31 Jan 2025' },
  { value: 'dd MMM yyyy',      label: 'dd MMM yyyy — 31 Jan 2025' },
  { value: 'MMM dd, yyyy',     label: 'MMM dd, yyyy — Jan 31, 2025 (United States)' },
  { value: 'MMMM dd, yyyy',    label: 'MMMM dd, yyyy — January 31, 2025' },
];

// ── Pagination Sizes ─────────────────────────────────────────────────────────
export const PAGINATION_SIZES = [
  { value: 10,  label: '10 rows per page' },
  { value: 25,  label: '25 rows per page' },
  { value: 50,  label: '50 rows per page' },
  { value: 100, label: '100 rows per page' },
];

// ── Export Formats ───────────────────────────────────────────────────────────
export const EXPORT_FORMATS = [
  { value: 1, label: 'PDF' },
  { value: 2, label: 'CSV' },
];

// ── Number Formats ───────────────────────────────────────────────────────────
// Controls how numbers are displayed e.g. 1,234.56 vs 1.234,56
export const NUMBER_FORMATS = [
  { value: 'en-US', label: '1,234.56 — Dot decimal, comma thousands (US, UK, Portugal)' },
  { value: 'de-DE', label: '1.234,56 — Comma decimal, dot thousands (Germany, Brazil, Italy)' },
  { value: 'fr-FR', label: '1 234,56 — Comma decimal, space thousands (France, Spain)' },
  { value: 'ch-CH', label: "1'234.56 — Dot decimal, apostrophe thousands (Switzerland)" },
];

// ── Budget Periods ───────────────────────────────────────────────────────────
// Used when building budgets per expense type
export const BUDGET_PERIODS = [
  { value: 1, label: 'Weekly' },
  { value: 2, label: 'Monthly' },
  { value: 3, label: 'Quarterly' },
  { value: 4, label: 'Yearly' },
];

// ── Recurring Intervals ──────────────────────────────────────────────────────
// Used when building recurring expenses
export const RECURRING_INTERVALS = [
  { value: 1, label: 'Daily' },
  { value: 2, label: 'Weekly' },
  { value: 3, label: 'Biweekly' },
  { value: 4, label: 'Monthly' },
  { value: 5, label: 'Quarterly' },
  { value: 6, label: 'Yearly' },
];