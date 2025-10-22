#pragma once
#include "pch.h"
#include "Core.h"

class ConfigManager {
public:
    static ConfigManager& Instance();

    void LoadConfig();
    void SaveConfig();
    void CreateDefaultConfig();

    const LoaderConfig& GetConfig() const;
    LoaderConfig& GetConfigMutable();

private:
    ConfigManager() = default;
    ~ConfigManager() = default;
    ConfigManager(const ConfigManager&) = delete;
    ConfigManager& operator=(const ConfigManager&) = delete;

    std::wstring GetConfigPath() const;
};