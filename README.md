# Project Rules

# Структура проекта

```text
Project/
│
└── Assets/
    ├── Art/
    ├── Audio/
    ├── Data/
    ├── Materials/
    ├── Prefabs/
    ├── Resources/
    ├── Scenes/
    ├── Scripts/
    ├── Settings/
    ├── UI/
    ├── VFX/
    └── Animations/
```

---

# Главные правила

## 1. Не переименовывать и не перемещать чужие файлы без согласования

Unity сильно зависит от meta-файлов.

Любое перемещение:
- ломает ссылки
- создает конфликты в Git
- может сломать сцены и prefab'ы

---

## 2. Одна задача — одна зона ответственности

### Программист работает в:

```text
Scripts/
Data/
Systems/
Core/
```

Также может:
- создавать prefab'ы
- создавать test scenes
- настраивать managers

---

### Интегратор работает в:

```text
Scenes/
Art/
UI/
Audio/
Materials/
VFX/
```

Также:
- собирает уровни
- расставляет объекты
- подключает анимации
- настраивает интерфейс

---

# Назначение папок

# Art/

Вся графика проекта.

## Структура

```text
Art/
├── Sprites/
├── Characters/
├── Environment/
├── Backgrounds/
├── Tiles/
├── Icons/
└── Fonts/
```

## Правила

- Не складывать все в одну папку
- Спрайты сортировать по назначению
- Не хранить PSD/исходники внутри Unity проекта

---

# Audio/

Все аудио-файлы проекта.

```text
Audio/
├── Music/
├── SFX/
├── Ambient/
└── Voices/
```

## Правила

### Music
Фоновая музыка.

### SFX
Звуковые эффекты.

### Ambient
Фоновые окружения.

### Voices
Озвучка персонажей.

---

# Data/

Игровые данные.

НЕ логика.

## Примеры

```text
Data/
├── Items/
├── Enemies/
├── Weapons/
├── Configs/
├── Balance/
└── Localization/
```

## Здесь хранятся

- ScriptableObject
- игровые настройки
- конфиги
- баланс
- локализация

---

# Materials/

Unity Materials и Shader материалы.

## Примеры

```text
Materials/
├── Sprites/
├── UI/
└── Effects/
```

---

# Prefabs/

Все prefab'ы проекта.

## Структура

```text
Prefabs/
├── Characters/
├── Enemies/
├── Environment/
├── UI/
├── Weapons/
├── Projectiles/
└── Interactive/
```

## Правила

- Не хранить prefab'ы в корне папки
- Один тип объектов = одна папка
- Не создавать дубликаты prefab'ов
- Не делать "Prefab_Final_Final2"

---

# Scripts/

Весь код проекта.

---

# Settings/

Технические настройки проекта.

## Примеры

```text
Settings/
├── Input/
├── URP/
├── Physics/
└── Addressables/
```

---

# UI/

Ассеты интерфейса.

## Структура

```text
UI/
├── Sprites/
├── Fonts/
├── Themes/
├── Layouts/
└── Animations/
```

---

# VFX/

Визуальные эффекты.

## Примеры

```text
VFX/
├── Particles/
├── Explosions/
├── HitEffects/
└── Shaders/
```

---

# Animations/

Анимации проекта.

## Структура

```text
Animations/
├── Characters/
├── Enemies/
├── UI/
└── Controllers/
```

---

# Правила именования

# Скрипты

## Формат

```text
EntityAction.cs
```

## Примеры

```text
PlayerMovement.cs
EnemyAI.cs
InventorySystem.cs
GameManager.cs
```

## Правила

- Один класс = один файл
- Имя файла = имя класса
- Без сокращений

---

# Prefab'ы

## Формат

```text
PF_Name
```

## Примеры

```text
PF_Player
PF_Slime
PF_Chest
```

---

# UI

## Формат

```text
UI_Name
```

## Примеры

```text
UI_Inventory
UI_MainMenu
UI_HealthBar
```

---

# Спрайты

## Формат

```text
SPR_Name
```

## Примеры

```text
SPR_PlayerIdle
SPR_SlimeAttack
SPR_BackgroundForest
```

---

# Материалы

## Формат

```text
MAT_Name
```

## Примеры

```text
MAT_Glow
MAT_PlayerOutline
```

---

# Анимации

## Формат

```text
AN_Name
```

## Примеры

```text
AN_PlayerRun
AN_SlimeAttack
```

---

# Перед commit

## Нужно

- проверить сцену
- проверить prefab overrides
- удалить мусор
- проверить Console

---

## Нельзя

- коммитить сломанные сцены
- коммитить ошибки в Console
- коммитить временные файлы

---

# Работа со сценами

## Важно

Unity плохо переносит одновременное редактирование одной сцены.

---

## Правила

### Если работаешь со сценой:

- предупреди второго человека
- не редактируйте одну сцену одновременно

---

# Работа с prefab'ами

## Правила

### Всегда:

- применять overrides осознанно
- не менять prefab без необходимости
- не делать nested prefab хаос

---

# Финальное правило

Если непонятно:
- куда класть файл
- как назвать объект
- можно ли менять структуру

Сначала согласовать, потом делать.