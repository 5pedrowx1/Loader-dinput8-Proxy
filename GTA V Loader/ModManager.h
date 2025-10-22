#pragma once
#include "pch.h"
#include "Core.h"
#include <filesystem>
#include <unordered_set>

namespace fs = std::filesystem;

class ModManager {
public:
    static ModManager& Instance();

    bool LoadMod(const fs::path& modPath, const std::string& type = "mod");
    bool UnloadMod(const std::string& modId);
    bool ReloadMod(const std::string& modId);

    void ScanAndLoadModsFolder();
    void ScanScriptsFolder();
    void ScanLoadedModules();

    void StartModuleScanThread();
    void StopModuleScanThread();

    std::string SerializeModsJSON() const;
    const std::vector<ModInfo>& GetLoadedMods() const;

private:
    ModManager();
    ~ModManager();
    ModManager(const ModManager&) = delete;
    ModManager& operator=(const ModManager&) = delete;

    bool CallModInitFunction(HMODULE hMod, const std::string& modName);
    bool CallModCleanupFunction(HMODULE hMod);

    bool IsModuleAlreadyTracked(HMODULE hModule) const;
    bool IsRelevantModule(const std::wstring& modPath) const;
    std::string DetermineModType(const std::wstring& modPath) const;

    void ModuleScanThread();

    std::thread m_ScanThread;
    std::atomic<bool> m_ScanRunning{ false };

    std::unordered_set<std::wstring> m_ScannedModules;
    std::mutex m_ScanCacheMutex;
};