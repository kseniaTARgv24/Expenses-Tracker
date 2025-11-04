# Expenses Tracker

Expenses Tracker is a cross-platform mobile application built with .NET MAUI that allows users to track their income and expenses in an intuitive and organized way.

## Features

### Core Functionality
- Add income and expense transactions with optional notes.
- Create custom categories for better organization of expenses.
- Automatic calculation and summarization of total income and expenses.
- Dynamic display of current month totals and transactions.

### Settings
- **Theme**: Light or Dark mode.
- **Language**: English or Russian, dynamically switchable without restarting the app.
- **Currency**: Set the preferred currency symbol, which updates dynamically across the app.

### Architecture
- **MVVM Pattern**: The application follows the Model-View-ViewModel architecture for separation of concerns.
  - **Models**: Represent the data structure for transactions, categories, and settings.
  - **ViewModels**: Handle the business logic and state management.
    - Uses `CommunityToolkit.Mvvm`:
      - `ObservableObject` for property change notifications.
      - `RelayCommand` for handling user interactions and commands.
  - **Views**: XAML pages bound to ViewModels for the user interface.

### Data Persistence
- **SQLite**: All transactions and categories are stored locally using `sqlite-net-pcl`.

## Usage
- Add new transactions and categories.
- View transactions for the current month along with summarized totals.
- Switch the application language and theme dynamically from the Settings page.
- Change the currency symbol, which will update all displayed monetary amounts.

## Potential Extensions
- View transactions for previous months (data is already stored in the database).
- Add charts and visualizations (e.g., Microcharts or other charting libraries).
- Implement login or local authentication to secure access to the app.
- Add a currency converter to automatically convert existing amounts when switching currencies.

## Technologies Used
- .NET MAUI
- SQLite (`sqlite-net-pcl`)
- CommunityToolkit.Mvvm
- XAML for UI
