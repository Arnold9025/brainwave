# 🌐 Guide d'Intégration Frontend - BrainWave API

Ce guide récapitule comment consommer l'API BrainWave depuis une application frontend (Flutter, React, etc.).

---

## 📍 Configuration de base

**Base URL (Production) :** `https://brainwave-x6kn.onrender.com`  
**Base URL (Local) :** `http://localhost:5200/api`

---

## 🔐 1. Authentification (`/auth`)

L'API utilise des **JWT (JSON Web Tokens)** pour sécuriser les accès.

| Action | Méthode | Endpoint | Body (JSON) | Retour |
| :--- | :--- | :--- | :--- | :--- |
| Inscription | `POST` | `/auth/register` | `{ "username", "email", "password" }` | `{ "token" }` |
| Connexion | `POST` | `/auth/login` | `{ "email", "password" }` | `{ "token" }` |

**Important :** Tous les autres endpoints nécessitent le header :  
`Authorization: Bearer <votre_token>`

---

## 🛠️ 2. Endpoints Principaux (Protégés)

| Action | Méthode | Endpoint | Body (JSON) |
| :--- | :--- | :--- | :--- |
| Lister Tâches | `GET` | `/tasks` | - |
| Créer Tâche | `POST` | `/tasks` | `{ "title", "description", "priority", "dueDate" }` |
| Modifier Tâche | `PUT` | `/tasks/{id}` | `{ "id", "title", "description", "priority", "isCompleted" }` |
| Voir Score | `GET` | `/scoring/{userId}` | - |

---

## 💙 3. Intégration Flutter (Exemple)

Voici comment structurer vos appels API en Flutter avec le package `http`.

### A. Service d'Authentification

```dart
import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class AuthService {
  final String baseUrl = "http://localhost:5200/api/auth"; // Changez pour l'URL de prod si besoin
  final storage = const FlutterSecureStorage();

  Future<String?> login(String email, String password) async {
    final response = await http.post(
      Uri.parse('$baseUrl/login'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode({'email': email, 'password': password}),
    );

    if (response.statusCode == 200) {
      final token = jsonDecode(response.body)['token'];
      await storage.write(key: 'jwt', value: token); // Stockage sécurisé
      return token;
    }
    return null;
  }
}
```

### B. Faire une requête authentifiée

```dart
Future<void> fetchTasks() async {
  final token = await storage.read(key: 'jwt'); // Récupération du token stocké
  
  final response = await http.get(
    Uri.parse('http://localhost:5200/api/tasks'),
    headers: {
      'Content-Type': 'application/json',
      'Authorization': 'Bearer $token', // JWT Header indispensable
    },
  );

  if (response.statusCode == 200) {
    // Parser les données JSON ici...
    print(response.body);
  } else if (response.statusCode == 401) {
    print("Session expirée ou non autorisée");
  }
}
```

---

## 🚀 4. Tests Rapides

1.  **Enregistrez un utilisateur** via `/api/auth/register`.
2.  **Récupérez le token** via `/api/auth/login`.
3.  **Testez un accès** :
    ```bash
    curl -H "Authorization: Bearer <token>" http://localhost:5200/api/tasks
    ```

---

## 🔒 Notes sur la sécurité
- **Stockage** : En Flutter, utilisez `flutter_secure_storage` au lieu de `shared_preferences` pour les tokens JWT afin de les chiffrer sur l'appareil.
- **Intercepteurs** : Pour des applications plus complexes, utilisez le package `dio` avec des intercepteurs pour ajouter automatiquement le token à chaque requête.
