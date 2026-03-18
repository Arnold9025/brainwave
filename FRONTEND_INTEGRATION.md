# 🌐 Guide d'Intégration Frontend - BrainWave API

Ce guide récapitule comment consommer l'API BrainWave depuis une application frontend (React, Vue, Next.js, etc.).

---

## 📍 Configuration de base

**Base URL (Production) :** `https://votre-app.onrender.com/api`  
**Base URL (Local) :** `http://localhost:5200/api`

---

## 🛠️ Endpoints Principaux

### 1. Gestion des Tâches (`/tasks`)

| Action | Méthode | Endpoint | Body (JSON) |
| :--- | :--- | :--- | :--- |
| Lister | `GET` | `/tasks?userId={uuid}` | - |
| Créer | `POST` | `/tasks` | `{ "title", "description", "priority", "userId", "dueDate" }` |
| Modifier | `PUT` | `/tasks/{id}` | `{ "id", "title", "description", "priority", "isCompleted" }` |
| Supprimer | `DELETE` | `/tasks/{id}` | - |

**Exemple de création (JavaScript) :**
```javascript
const createTask = async (taskData) => {
  const response = await fetch(`${API_URL}/tasks`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(taskData)
  });
  return await response.json(); // Retourne l'ID de la tâche créée
};
```

### 2. Gestion des Objectifs (`/objectives`)

| Action | Méthode | Endpoint | Body (JSON) |
| :--- | :--- | :--- | :--- |
| Lister | `GET` | `/objectives?userId={uuid}` | - |
| Créer | `POST` | `/objectives` | `{ "title", "description", "deadline", "userId" }` |

### 3. Score & Recommendations

| Action | Méthode | Endpoint | Description |
| :--- | :--- | :--- | :--- |
| Voir le score | `GET` | `/scoring/{userId}` | Récupère le Productivity Score actuel. |
| Recalculer | `POST` | `/scoring/{userId}/recalculate` | Force la mise à jour du score. |
| Suggestions | `GET` | `/recommendations/{userId}` | Retourne des tâches suggérées. |

---

## 👤 Gestion de l'Utilisateur

**Important :** Pour le moment, l'API utilise une stratégie de **"Auto-User Creation"**.
- Vous pouvez envoyer n'importe quel `Guid` (UUID v4) comme `userId`.
- Si l'utilisateur n'existe pas en base, l'API le créera automatiquement lors de la première action (création de tâche/objectif).

**Format du UserId :** `3fa85f64-5717-4562-b3fc-2c963f66afa6` (Exemple)

---

## 🚀 Exemple Complet (React + Axios)

```javascript
import axios from 'axios';

const api = axios.create({
  baseURL: 'https://votre-app.onrender.com/api'
});

// Récupérer les tâches et le score
export const getUserDashboard = async (userId) => {
  const [tasks, score] = await Promise.all([
    api.get(`/tasks?userId=${userId}`),
    api.get(`/scoring/${userId}`)
  ]);
  
  return {
    tasks: tasks.data,
    productivityScore: score.data.score
  };
};
```

---

## 🔒 Notes sur la sécurité (CORS)

L'API est configurée pour accepter les requêtes de n'importe quelle origine (`AllowAnyOrigin`). Vous ne devriez pas rencontrer d'erreurs CORS lors de vos tests.
